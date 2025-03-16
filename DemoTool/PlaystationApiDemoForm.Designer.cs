namespace DemoTool;

public sealed partial class PlaystationApiDemoForm
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
        this.connect_button = new System.Windows.Forms.Button();
        this.attach_button = new System.Windows.Forms.Button();
        this.read_button = new System.Windows.Forms.Button();
        this.write_button = new System.Windows.Forms.Button();
        this.ccapiRadioButton = new System.Windows.Forms.RadioButton();
        this.tmapiRadioButton = new System.Windows.Forms.RadioButton();
        this.ps3mapiRadioButton = new System.Windows.Forms.RadioButton();
        this.SuspendLayout();
        // 
        // connect_button
        // 
        this.connect_button.Location = new System.Drawing.Point(37, 63);
        this.connect_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.connect_button.Name = "connect_button";
        this.connect_button.Size = new System.Drawing.Size(100, 47);
        this.connect_button.TabIndex = 0;
        this.connect_button.Text = "Connect";
        this.connect_button.UseVisualStyleBackColor = true;
        this.connect_button.Click += new System.EventHandler(this.Connect_Button_Click);
        // 
        // attach_button
        // 
        this.attach_button.Location = new System.Drawing.Point(149, 63);
        this.attach_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.attach_button.Name = "attach_button";
        this.attach_button.Size = new System.Drawing.Size(100, 47);
        this.attach_button.TabIndex = 1;
        this.attach_button.Text = "Attach";
        this.attach_button.UseVisualStyleBackColor = true;
        this.attach_button.Click += new System.EventHandler(this.Attach_Button_Click);
        // 
        // read_button
        // 
        this.read_button.Location = new System.Drawing.Point(37, 146);
        this.read_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.read_button.Name = "read_button";
        this.read_button.Size = new System.Drawing.Size(100, 47);
        this.read_button.TabIndex = 2;
        this.read_button.Text = "Read";
        this.read_button.UseVisualStyleBackColor = true;
        this.read_button.Click += new System.EventHandler(this.Read_Button_Click);
        // 
        // write_button
        // 
        this.write_button.Location = new System.Drawing.Point(149, 146);
        this.write_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.write_button.Name = "write_button";
        this.write_button.Size = new System.Drawing.Size(100, 47);
        this.write_button.TabIndex = 3;
        this.write_button.Text = "Write";
        this.write_button.UseVisualStyleBackColor = true;
        this.write_button.Click += new System.EventHandler(this.Write_Button_Click);
        // 
        // ccapiRadioButton
        // 
        this.ccapiRadioButton.AutoSize = true;
        this.ccapiRadioButton.Location = new System.Drawing.Point(21, 15);
        this.ccapiRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.ccapiRadioButton.Name = "CCAPI";
        this.ccapiRadioButton.Size = new System.Drawing.Size(52, 17);
        this.ccapiRadioButton.TabIndex = 4;
        this.ccapiRadioButton.TabStop = true;
        this.ccapiRadioButton.Text = "Ccapi";
        this.ccapiRadioButton.UseVisualStyleBackColor = true;
        this.ccapiRadioButton.CheckedChanged += new System.EventHandler(this.CCAPRadioButton_CheckedChanged);
        // 
        // tmapiRadioButton
        // 
        this.tmapiRadioButton.AutoSize = true;
        this.tmapiRadioButton.Location = new System.Drawing.Point(115, 15);
        this.tmapiRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.tmapiRadioButton.Name = "tmapiRadioButton";
        this.tmapiRadioButton.Size = new System.Drawing.Size(54, 17);
        this.tmapiRadioButton.TabIndex = 5;
        this.tmapiRadioButton.TabStop = true;
        this.tmapiRadioButton.Text = "Tmapi";
        this.tmapiRadioButton.UseVisualStyleBackColor = true;
        this.tmapiRadioButton.CheckedChanged += new System.EventHandler(this.TMAPIRadioButton_CheckedChanged);
        // 
        // ps3mapiRadioButton
        // 
        this.ps3mapiRadioButton.AutoSize = true;
        this.ps3mapiRadioButton.Location = new System.Drawing.Point(211, 15);
        this.ps3mapiRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.ps3mapiRadioButton.Name = "ps3mapiRadioButton";
        this.ps3mapiRadioButton.Size = new System.Drawing.Size(67, 17);
        this.ps3mapiRadioButton.TabIndex = 6;
        this.ps3mapiRadioButton.TabStop = true;
        this.ps3mapiRadioButton.Text = "PS3mapi";
        this.ps3mapiRadioButton.UseVisualStyleBackColor = true;
        this.ps3mapiRadioButton.CheckedChanged += new System.EventHandler(this.PS3mapiRadioButton_CheckedChanged);
        // 
        // PlaystationApiDemoForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(302, 235);
        this.Controls.Add(this.ps3mapiRadioButton);
        this.Controls.Add(this.tmapiRadioButton);
        this.Controls.Add(this.ccapiRadioButton);
        this.Controls.Add(this.write_button);
        this.Controls.Add(this.read_button);
        this.Controls.Add(this.attach_button);
        this.Controls.Add(this.connect_button);
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "PlaystationApiDemoForm";
        this.Text = "Demo";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button connect_button;
    private System.Windows.Forms.Button attach_button;
    private System.Windows.Forms.Button read_button;
    private System.Windows.Forms.Button write_button;
    private System.Windows.Forms.RadioButton ccapiRadioButton;
    private System.Windows.Forms.RadioButton tmapiRadioButton;
    private System.Windows.Forms.RadioButton ps3mapiRadioButton;
}

