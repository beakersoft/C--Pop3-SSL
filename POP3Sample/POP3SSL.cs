/***************************************************************************************************************************
'Author: Luke Niland
'Language: c#
'Version: 1.0
'Description: Class to connent to a pop3 mail server over an SSL connection. Also includes simple functions for getting
 the number of messages, and extracting header information
'See RFC 1939 for details of the pop3 specification
'****************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Globalization;
using System.Threading;

namespace POP3Sample
{
    class POP3SSL
    {
        //vars to use for the session
        public bool isConnected { get; set; }       //have we got a connection
        public int ErrorLevel { get; set; }         //an error level we can use. 0 is no errors
        public string ErrorText { get; set; }       //message text of the error        

        private string myServerName;                //servername/address
        private int myServerPort;                   //server port
        private string myUserName;                  //username to connect to 
        private string myPassword;                  //password to use for the user
        private TcpClient myPopTCPClient;           //socket to handle the connection
        private SslStream myPopSSLStream;           //SSL stream to talk to the server with
        private StreamReader mySSLReader;           //stream to read the response's into
        private StreamWriter mySSLWritter;          //stream to write command to the ssl stream with
        private string myServerResponse;            //what the server responded with

        public POP3SSL(string ServerName, int ServerPort, string Username, string Password)
        {
            //set initial state to not connected, and set the servername, port and user details
            isConnected = false;
            myServerName = ServerName;
            myServerPort = ServerPort;
            myUserName = Username;
            myPassword = Password;
        }

        //try and open the server connection
        public void Connect()
        {
            //this is the complex bit. TCP connection to the server, then create a new ssl connection over that stream, and try and valididate the certs
            try
            {
                myPopTCPClient = new TcpClient(myServerName, myServerPort);
            }
            catch (Exception e)
            {
                ErrorLevel = 1;
                ErrorText = "Problem connecting to the pop3 server - " + e.Message;
                return;
            }            
            myPopSSLStream = new SslStream(myPopTCPClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), new LocalCertificateSelectionCallback(SelectLocalCertificate));

            //now try and authenticate over the stream
            myPopSSLStream.AuthenticateAsClient(myServerName);

            //create our reader/writter objects on the stream, so we can talk to it
            mySSLReader = new StreamReader(myPopSSLStream);
            mySSLWritter = new StreamWriter(myPopSSLStream);

            //now see what result we got back from the connection
            myServerResponse = mySSLReader.ReadLine();

            //the response should always start with +OK (the standard pop3 response), then possabley some other info. We will just check for +OK
            if (myServerResponse.Substring(0, 3) == "+OK")
                isConnected = true;
            else
            {
                ErrorLevel = 2;
                ErrorText = "Problem creating SSL Connection. Server Response was - " + myServerResponse;
                return;
            }

            //now we have got this far, lets try and send the username and password to login
            if (!SendCommand("USER " + myUserName))
            {
                ErrorLevel = 3;
                ErrorText = "Problem with user " + ErrorText;
                return;
            }
            if (!SendCommand("PASS " + myPassword))
            {
                ErrorLevel = 3;
                ErrorText = "Problem with password " + ErrorText;
                return;
            }            
        }

        //see how many messages are on the server
        public int GetNoOfMessages(){

            //first make sure we are connected
            if (isConnected == false)
                return 0;

            //send the stat command to the server
            if (!SendCommand("STAT"))
                return 0;

            //the response comes back as number of messages then the total size of the messages (in bytes)
            string[] StatResponse = myServerResponse.Split(' ');
            return Int32.Parse(StatResponse[1]);
            
        }

        //get the content of a message
        public string GetMessageContent(int MessageIndex)
        {
            //first make sure we are connected
            if (isConnected == false)
                return string.Empty;

            //use the RETR command with the message number to get the message
            if (!SendCommand("RETR " + MessageIndex))
            {
                ErrorLevel = 1;
                ErrorText = "Problem getting message content";
                return "";
            }

            //the first line we get back is the +OK, the rest of the message needs reading in, up to a single 
            //line containing only a fullpoint(chr46)
            string TempHolder;
            string EmailMessage = "";
            TempHolder = mySSLReader.ReadLine();

            while (TempHolder != ".")
            {
                EmailMessage += TempHolder + Environment.NewLine;
                TempHolder = mySSLReader.ReadLine();
            }
            
            //return the message
            return EmailMessage;

        }

        //get a header type out of the email (like the subject)
        public string ParseMessageHeader(string EmailMessage, string HeaderType)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            int StartPoint,LenOfData;
            string HeaderData;

            //check for all connatations of the type. Make it all upper, all lower and real case
            StartPoint = EmailMessage.IndexOf(HeaderType) - 1;
            if (StartPoint <= 0)            
                StartPoint = EmailMessage.IndexOf(textInfo.ToTitleCase(HeaderType)) - 1;
            if (StartPoint <= 0)
                StartPoint = EmailMessage.IndexOf(textInfo.ToLower(HeaderType)) - 1;
            if (StartPoint <= 0)
                StartPoint = EmailMessage.IndexOf(textInfo.ToUpper(HeaderType)) - 1;

            //get the lengh of the string we want
            LenOfData = EmailMessage.IndexOf(Environment.NewLine, StartPoint) - StartPoint;
            //now extract the header
            HeaderData = EmailMessage.Substring(StartPoint, LenOfData);

            //clean the string up a bit
            return HeaderData.Replace(Environment.NewLine, "");
        }

        //close the connection to the server, and then distroy our objects
        public void Disconnect()
        {
            //kill the connection from the pop3 server
            SendCommand("QUIT");

            //now set our status to disconected, and clean up the objects
            isConnected = false;
            mySSLReader.Close();
            mySSLWritter.Close();
            myPopSSLStream.Close();
            myPopTCPClient.Close();

        }

        //send a command to the server
        private bool SendCommand(string POPcommand)
        {            
            //send the command, making sure we flush it
            mySSLWritter.WriteLine(POPcommand);
            mySSLWritter.Flush();

            //read the response in
            myServerResponse = mySSLReader.ReadLine();

            //check if the response is null, seems to happen if you send a malformed command
            if (myServerResponse == null)
                return false;

            //did we have a good response
            if (myServerResponse.Substring(0, 3) == "+OK")
                return true;
            else
            {
                ErrorText = myServerResponse;
                return false;
            }
        }

        //try and find a valid certifcate. Found on the MSDN forums
        private X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            // Check to see if there are any acceptable issuers and local certificates
            if (acceptableIssuers != null && acceptableIssuers.Length > 0 && localCertificates != null && localCertificates.Count > 0)
            {
                // Loop through the certificates
                foreach (X509Certificate certificate in localCertificates)
                {
                    // Check to see if this cert contains an acceptable issuer
                    if (Array.IndexOf(acceptableIssuers, certificate.Issuer) != -1)
                        return certificate;
                }
            }

            // If there were no acceptable issuers, just return the first certificate found
            if (localCertificates != null && localCertificates.Count > 0)
                return localCertificates[0];

            // Nothing was found
            return null;
        }

        //try and validate the cert we have got from the server. Again found on the msdn forums
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Success
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Failure
            return false;
        }





    }
}
