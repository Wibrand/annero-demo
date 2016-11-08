using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetInformationFromCirrus
{

    public class DTO { }
    public class LoginRequest : DTO
    {
        public readonly string messageType = "LoginRequest";
        public long timeSent { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }


    public class GetUnitsRequest : DTO
    {
        public readonly string messageType = "GetUnitsRequest";
        public long timeSent { get; set; }
        public Locationaddress locationAddress { get; set; }
    }

    public class Locationaddress
    {
        public string resourceType { get; set; }
        public string locationId { get; set; }
    }


    public class GetUnitsResponse
    {
        public string messageType { get; set; }
        public Responsecode responseCode { get; set; }
        public long timeSent { get; set; }
        public UnitDTO[] list { get; set; }
        public Locationaddress2 locationAddress { get; set; }
    }

    public class Locationaddress2
    {
        public string resourceType { get; set; }
        public long timeCreated { get; set; }
        public string locationId { get; set; }
        public string serverDid { get; set; }
    }

    public class UnitDTO
    {
        public string resourceType { get; set; }
        public long timeCreated { get; set; }
        public Unitaddress unitAddress { get; set; }
        public string productType { get; set; }
        public Lifecyclestate lifeCycleState { get; set; }
        public bool isChassis { get; set; }
        public string chassisDid { get; set; }
        public Unittypefixed unitTypeFixed { get; set; }
        public Unittypeconfigured unitTypeConfigured { get; set; }
        public bool isNameSetByUser { get; set; }
        public string nameSetByUser { get; set; }
        public string defaultNameSetBySystem { get; set; }
        public string userId { get; set; }
        public Unitacl unitAcl { get; set; }
        public int subunitIdentifier { get; set; }
    }

    public class Unitaddress
    {
        public string resourceType { get; set; }
        public long timeCreated { get; set; }
        public string did { get; set; }
        public string locationId { get; set; }
        public string serverDid { get; set; }
    }

    public class Lifecyclestate
    {
        public string resourceType { get; set; }
        public string name { get; set; }
    }

    public class Unittypefixed
    {
        public string resourceType { get; set; }
        public string name { get; set; }
    }

    public class Unittypeconfigured
    {
        public string resourceType { get; set; }
        public string name { get; set; }
    }

    public class Unitacl
    {
        public string resourceType { get; set; }
        public string name { get; set; }
    }


    public class MessageResponse
    {
        public string messageType { get; set; }
        public Responsecode responseCode { get; set; }
        public long timeSent { get; set; }
    }

    public class Responsecode
    {
        public string resourceType { get; set; }
        public string name { get; set; }
    }

    public class Messageidentifier
    {
        public string resourceType { get; set; }
        public long timeCreated { get; set; }
        public string messageId { get; set; }
    }

    public class AdminMessageSB
    {
        public readonly string messageType = "admin";
        public string externalAssetid { get; set; }
        public string logicalName { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string areaId { get; set; }
        public string floorId { get; set; }
        public string buildingId { get; set; }
    }
}
