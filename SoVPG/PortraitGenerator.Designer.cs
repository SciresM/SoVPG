namespace SoVPG
{
    partial class PortraitGenerator
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
            this.PB_Portrait = new System.Windows.Forms.PictureBox();
            this.CHK_SweatDrop = new System.Windows.Forms.CheckBox();
            this.CHK_Blush = new System.Windows.Forms.CheckBox();
            this.CB_Emotion = new System.Windows.Forms.ComboBox();
            this.LBL_Emotions = new System.Windows.Forms.Label();
            this.LBL_Character = new System.Windows.Forms.Label();
            this.CB_Character = new System.Windows.Forms.ComboBox();
            this.LBL_PortraitStyle = new System.Windows.Forms.Label();
            this.CB_PortraitStyle = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Portrait)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_Portrait
            // 
            this.PB_Portrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_Portrait.ImageLocation = "";
            this.PB_Portrait.Location = new System.Drawing.Point(6, 6);
            this.PB_Portrait.Name = "PB_Portrait";
            this.PB_Portrait.Size = new System.Drawing.Size(512, 512);
            this.PB_Portrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_Portrait.TabIndex = 1;
            this.PB_Portrait.TabStop = false;
            this.PB_Portrait.Click += new System.EventHandler(this.SaveImage);
            // 
            // CHK_SweatDrop
            // 
            this.CHK_SweatDrop.AutoSize = true;
            this.CHK_SweatDrop.Location = new System.Drawing.Point(474, 530);
            this.CHK_SweatDrop.Name = "CHK_SweatDrop";
            this.CHK_SweatDrop.Size = new System.Drawing.Size(38, 17);
            this.CHK_SweatDrop.TabIndex = 47;
            this.CHK_SweatDrop.Text = "汗";
            this.CHK_SweatDrop.UseVisualStyleBackColor = true;
            this.CHK_SweatDrop.CheckedChanged += new System.EventHandler(this.UpdateSweat);
            // 
            // CHK_Blush
            // 
            this.CHK_Blush.AutoSize = true;
            this.CHK_Blush.Location = new System.Drawing.Point(437, 530);
            this.CHK_Blush.Name = "CHK_Blush";
            this.CHK_Blush.Size = new System.Drawing.Size(38, 17);
            this.CHK_Blush.TabIndex = 46;
            this.CHK_Blush.Text = "照";
            this.CHK_Blush.UseVisualStyleBackColor = true;
            this.CHK_Blush.CheckedChanged += new System.EventHandler(this.UpdateBlush);
            // 
            // CB_Emotion
            // 
            this.CB_Emotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Emotion.FormattingEnabled = true;
            this.CB_Emotion.Location = new System.Drawing.Point(374, 527);
            this.CB_Emotion.Name = "CB_Emotion";
            this.CB_Emotion.Size = new System.Drawing.Size(59, 21);
            this.CB_Emotion.TabIndex = 45;
            this.CB_Emotion.SelectedValueChanged += new System.EventHandler(this.UpdateEmotion);
            // 
            // LBL_Emotions
            // 
            this.LBL_Emotions.AutoSize = true;
            this.LBL_Emotions.Location = new System.Drawing.Point(322, 531);
            this.LBL_Emotions.Name = "LBL_Emotions";
            this.LBL_Emotions.Size = new System.Drawing.Size(53, 13);
            this.LBL_Emotions.TabIndex = 44;
            this.LBL_Emotions.Text = "Emotions:";
            // 
            // LBL_Character
            // 
            this.LBL_Character.AutoSize = true;
            this.LBL_Character.Location = new System.Drawing.Point(12, 531);
            this.LBL_Character.Name = "LBL_Character";
            this.LBL_Character.Size = new System.Drawing.Size(56, 13);
            this.LBL_Character.TabIndex = 34;
            this.LBL_Character.Text = "Character:";
            // 
            // CB_Character
            // 
            this.CB_Character.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Character.FormattingEnabled = true;
            this.CB_Character.Location = new System.Drawing.Point(69, 528);
            this.CB_Character.Name = "CB_Character";
            this.CB_Character.Size = new System.Drawing.Size(110, 21);
            this.CB_Character.TabIndex = 33;
            this.CB_Character.SelectedValueChanged += new System.EventHandler(this.UpdateCharacter);
            // 
            // LBL_PortraitStyle
            // 
            this.LBL_PortraitStyle.AutoSize = true;
            this.LBL_PortraitStyle.Location = new System.Drawing.Point(179, 530);
            this.LBL_PortraitStyle.Name = "LBL_PortraitStyle";
            this.LBL_PortraitStyle.Size = new System.Drawing.Size(69, 13);
            this.LBL_PortraitStyle.TabIndex = 32;
            this.LBL_PortraitStyle.Text = "Portrait Style:";
            // 
            // CB_PortraitStyle
            // 
            this.CB_PortraitStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PortraitStyle.FormattingEnabled = true;
            this.CB_PortraitStyle.Location = new System.Drawing.Point(249, 527);
            this.CB_PortraitStyle.Name = "CB_PortraitStyle";
            this.CB_PortraitStyle.Size = new System.Drawing.Size(67, 21);
            this.CB_PortraitStyle.TabIndex = 31;
            this.CB_PortraitStyle.SelectedValueChanged += new System.EventHandler(this.UpdateStyle);
            // 
            // PortraitGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 561);
            this.Controls.Add(this.CHK_SweatDrop);
            this.Controls.Add(this.CHK_Blush);
            this.Controls.Add(this.CB_Emotion);
            this.Controls.Add(this.LBL_Emotions);
            this.Controls.Add(this.LBL_Character);
            this.Controls.Add(this.CB_Character);
            this.Controls.Add(this.LBL_PortraitStyle);
            this.Controls.Add(this.CB_PortraitStyle);
            this.Controls.Add(this.PB_Portrait);
            this.MaximumSize = new System.Drawing.Size(540, 600);
            this.MinimumSize = new System.Drawing.Size(540, 600);
            this.Name = "PortraitGenerator";
            this.Text = "Shadows of Valentia Portrait Generator";
            ((System.ComponentModel.ISupportInitialize)(this.PB_Portrait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_Portrait;
        private System.Windows.Forms.CheckBox CHK_SweatDrop;
        private System.Windows.Forms.CheckBox CHK_Blush;
        private System.Windows.Forms.ComboBox CB_Emotion;
        private System.Windows.Forms.Label LBL_Emotions;
        private System.Windows.Forms.Label LBL_Character;
        private System.Windows.Forms.ComboBox CB_Character;
        private System.Windows.Forms.Label LBL_PortraitStyle;
        private System.Windows.Forms.ComboBox CB_PortraitStyle;
    }
}

