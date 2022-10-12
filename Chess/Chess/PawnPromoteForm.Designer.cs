namespace Chess
{
    partial class PawnPromoteForm
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
            this.promoteComboBox = new System.Windows.Forms.ComboBox();
            this.promoteLabelBox = new System.Windows.Forms.Label();
            this.submitPromotionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // promoteComboBox
            // 
            this.promoteComboBox.FormattingEnabled = true;
            this.promoteComboBox.Items.AddRange(new object[] {
            "Queen",
            "Rook",
            "Bishop",
            "Knight"});
            this.promoteComboBox.Location = new System.Drawing.Point(186, 81);
            this.promoteComboBox.Name = "promoteComboBox";
            this.promoteComboBox.Size = new System.Drawing.Size(408, 28);
            this.promoteComboBox.TabIndex = 0;
            // 
            // promoteLabelBox
            // 
            this.promoteLabelBox.AutoSize = true;
            this.promoteLabelBox.Location = new System.Drawing.Point(260, 42);
            this.promoteLabelBox.Name = "promoteLabelBox";
            this.promoteLabelBox.Size = new System.Drawing.Size(235, 20);
            this.promoteLabelBox.TabIndex = 1;
            this.promoteLabelBox.Text = "What do you want to promote to?";
            // 
            // submitPromotionButton
            // 
            this.submitPromotionButton.Location = new System.Drawing.Point(270, 149);
            this.submitPromotionButton.Name = "submitPromotionButton";
            this.submitPromotionButton.Size = new System.Drawing.Size(225, 29);
            this.submitPromotionButton.TabIndex = 2;
            this.submitPromotionButton.Text = "Submit";
            this.submitPromotionButton.UseVisualStyleBackColor = true;
            this.submitPromotionButton.Click += new System.EventHandler(this.submitPromotionButton_Click);
            // 
            // PawnPromoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 190);
            this.Controls.Add(this.submitPromotionButton);
            this.Controls.Add(this.promoteLabelBox);
            this.Controls.Add(this.promoteComboBox);
            this.Name = "PawnPromoteForm";
            this.Text = "PawnPromoteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox promoteComboBox;
        private Label promoteLabelBox;
        private Button submitPromotionButton;
    }
}