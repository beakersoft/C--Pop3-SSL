namespace POP3Sample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.MaskedTextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.NumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblPort);
            this.grpInfo.Controls.Add(this.txtServerPort);
            this.grpInfo.Controls.Add(this.txtUsername);
            this.grpInfo.Controls.Add(this.txtPassword);
            this.grpInfo.Controls.Add(this.txtServer);
            this.grpInfo.Controls.Add(this.lblPassword);
            this.grpInfo.Controls.Add(this.lblUsername);
            this.grpInfo.Controls.Add(this.lblServer);
            this.grpInfo.Location = new System.Drawing.Point(12, 12);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(818, 106);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Connection Information";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(288, 47);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 20);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = "beakersoft@gmail.com";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(553, 46);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(10, 47);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(203, 20);
            this.txtServer.TabIndex = 4;
            this.txtServer.Text = "pop.gmail.com";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(559, 30);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(285, 30);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(7, 30);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(69, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "POP3 Server";
            // 
            // lstInfo
            // 
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.Location = new System.Drawing.Point(12, 124);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(818, 134);
            this.lstInfo.TabIndex = 1;
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(755, 264);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 2;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(219, 47);
            this.txtServerPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(51, 20);
            this.txtServerPort.TabIndex = 7;
            this.txtServerPort.Value = new decimal(new int[] {
            995,
            0,
            0,
            0});
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(216, 30);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 296);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.lstInfo);
            this.Controls.Add(this.grpInfo);
            this.Name = "Form1";
            this.Text = "POP3 Over SSL Sample";
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.MaskedTextBox txtPassword;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.ListBox lstInfo;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.NumericUpDown txtServerPort;
    }
}

