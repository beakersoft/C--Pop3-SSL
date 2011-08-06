using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POP3Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            cmdConnect.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            lstInfo.Items.Clear();
            
            {
                //use and ssl connection and create connections
                POP3SSL objPop3SSL = new POP3SSL(txtServer.Text, (int)txtServerPort.Value, txtUsername.Text, txtPassword.Text);
                objPop3SSL.Connect();

                //check we connected
                if (objPop3SSL.ErrorLevel != 0)
                {
                    lstInfo.Items.Add(DateTime.Now + " - " + objPop3SSL.ErrorText.ToString());
                    MessageBox.Show(objPop3SSL.ErrorText.ToString());
                    cmdConnect.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    return;
                }

                lstInfo.Items.Add(DateTime.Now + " - Connected and Logged in over SSL");

                //got this far, so lets get some messages                
                int NoOfMessage = objPop3SSL.GetNoOfMessages();
                string MessageHeaders, MessageSubject, MessageFrom;

                if (NoOfMessage > 0)
                {
                    //we have some messages, so loop through each one and get some simple info (only get the top 10)
                    for (int i = 1; i < 10; i++)
                    {
                        MessageHeaders = objPop3SSL.GetMessageContent(i);
                        MessageSubject = objPop3SSL.ParseMessageHeader(MessageHeaders, "Subject: ");
                        MessageFrom = objPop3SSL.ParseMessageHeader(MessageHeaders, "From: ");
                        lstInfo.Items.Add(DateTime.Now + " - " + MessageSubject + " " + MessageFrom);
                    }
                }
                else
                    lstInfo.Items.Add(DateTime.Now + " - No Messages found");

                //clean up
                objPop3SSL.Disconnect();
                lstInfo.Items.Add(DateTime.Now + " - Disconected from Server");
                Cursor.Current = Cursors.Default;
                cmdConnect.Enabled = true;
            }

        }
    }
}
