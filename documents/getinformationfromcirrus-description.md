# GetInformationFromCirrus
This is a Windows Console Application that get sensor information from Yanzi (their api is called Cirrus) and sends it to the Admin Service Bus Topic. 

## Parameters
<table>
    <tr>
        <td>u</td>
        <td>UserName</td>
        <td>This is the username for Yanzis service</td>
    </tr>
    <tr>
        <td>p</td>
        <td>Password</td>
        <td>This is the password for Yanzis service</td>
    </tr>
    <tr>
        <td>l</td>
        <td>LocationId</td>
        <td>This is the location of the installed sensors in Yanzis service</td>
    </tr>
    <tr>
        <td>f</td>
        <td>Filename to a Json file</td>
        <td>Optional: Stores the information from Yanzis service into a file</td>
    </tr>
    <tr>
        <td>s</td>
        <td>Service Bus Connection String</td>
        <td>Optional: The Connection String to the Topic that sends admin
        messages to the webjob admin-worker</td>
    </tr>
</table>

## Example

-u kalle@olle.com 
-p P@ssword1 
-l 4578 
-f yanzi.json 
-s Endpoint=sb://xxxxxx.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=sddQfj5Zic/xwsCzyoz1h6ziUYukI76AMAo8txxxxI=;EntityPath=admin-messages