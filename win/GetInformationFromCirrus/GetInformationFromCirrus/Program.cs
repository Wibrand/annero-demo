using CommandLine;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.IO;
using WebSocketSharp;

namespace GetInformationFromCirrus
{
    /*
     * Websocket info: http://sta.github.io/websocket-sharp/
     * Command Line Parser: https://github.com/cosmo0/commandline
     * 
     */

    class Program
    {
        const string CIRRUS_API_ADDRESS = "wss://cirrus5.yanzi.se/cirrusAPI";
        static WebSocket ws = new WebSocket(CIRRUS_API_ADDRESS);
        static ParserResult<Options> options;
        static TopicClient sbClient;


        static void Main(string[] args)
        {
            options = CommandLine.Parser.Default.ParseArguments<Options>(args);

            if (options.Value.ServiceBusConnectionString != null)
            {
                sbClient = TopicClient.CreateFromConnectionString(options.Value.ServiceBusConnectionString);

            }
            ws.OnMessage += ReadMessage;

            ws.Connect();

            ws.Send(
                    SerializeMessages(
                            new LoginRequest()
                            {
                                password = options.Value.Password,
                                username = options.Value.UserName
                            }
                        )
                );

            Console.WriteLine("<any key> to exit");
            Console.ReadKey(true);
            ws.Close();

        }

        static void ReadMessage(object sender, MessageEventArgs e)
        {
            var json = JsonConvert.DeserializeObject<MessageResponse>(e.Data);

            if (json.responseCode.name.IndexOf("error") > -1)
            {
                Console.WriteLine($"Error: {json.messageType} - {json.responseCode.name}");
                return;
            }

            switch (json.messageType)
            {
                case "LoginResponse":
                    GetUnitInformation();
                    break;
                case "GetUnitsResponse":
                    ParseUnitInformation(e.Data);
                    break;
                default:
                    break;
            }

        }

        static void GetUnitInformation()
        {
            ws.Send(
                    SerializeMessages(
                            new GetUnitsRequest()
                            {
                                locationAddress = new Locationaddress()
                                {
                                    locationId = options.Value.LocationId,
                                    resourceType = "LocationAddress"
                                }
                            }
                        )
                );

        }

        static void ParseUnitInformation(string jsonString)
        {

            var json = JsonConvert.DeserializeObject<GetUnitsResponse>(jsonString);
            PrintUnitInformation(json);

            if (options.Value.FileName != null)
            {
                SaveToFile(json);
            }


            if (options.Value.ServiceBusConnectionString != null)
            {
                ParseAndWriteMessageToSB(json);
            }
        }

        static void SaveToFile(GetUnitsResponse response)
        {
            File.Delete(options.Value.FileName);
            var f = File.AppendText(options.Value.FileName);

            foreach (var item in response.list)
            {
                if(!IsTypeExcluded(item))
                {
                    f.WriteLine(JsonConvert.SerializeObject(item));
                }
            }

            f.Close();

        }

        static bool IsTypeExcluded(UnitDTO item)
        {
            if(options.Value.ExcludeTypes != null)
            {
                if(options.Value.ExcludeTypes.IndexOf(item.unitTypeFixed.name) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static void PrintUnitInformation(GetUnitsResponse response)
        {

            foreach (var item in response.list)
            {
                if (!IsTypeExcluded(item))
                {
                    Console.WriteLine($"DID: {item.unitAddress.did}\t{item.unitTypeFixed.name}\t{item.isNameSetByUser}");
                }
            }
        }

        static string SerializeMessages(DTO message)
        {
            return JsonConvert.SerializeObject(message);
        }

        static void ParseAndWriteMessageToSB(GetUnitsResponse response)
        {
            foreach (var item in response.list)
            {
                if (!IsTypeExcluded(item))
                {

                    AdminMessageSB m = new AdminMessageSB()
                    {
                        externalAssetid = item.unitAddress.did,
                        type = item.unitTypeFixed.name

                    };

                    if (item.isNameSetByUser)
                    {
                        m.logicalName = item.nameSetByUser;
                        Console.WriteLine($"asset: {m.logicalName} added");
                    }
                    else
                    {
                        m.logicalName = item.defaultNameSetBySystem;
                    }

                    sbClient.Send(new BrokeredMessage(JsonConvert.SerializeObject(m)));
                }
            }
        }
    }
}
