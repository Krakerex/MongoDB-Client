namespace Projekt
{
    partial class Dane
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dane));
            this.pracownik_data = new System.Windows.Forms.DataGridView();
            this.refresh_button = new System.Windows.Forms.Button();
            this.deleteRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pracownik_data)).BeginInit();
            this.SuspendLayout();
            // 
            // pracownik_data
            // 
            this.pracownik_data.AllowUserToAddRows = false;
            this.pracownik_data.AllowUserToDeleteRows = false;
            this.pracownik_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pracownik_data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.pracownik_data.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pracownik_data.Location = new System.Drawing.Point(12, 12);
            this.pracownik_data.Name = "pracownik_data";
            this.pracownik_data.ReadOnly = true;
            this.pracownik_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.pracownik_data.Size = new System.Drawing.Size(776, 277);
            this.pracownik_data.TabIndex = 0;
            this.pracownik_data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pracownik_data_CellClick);
            this.pracownik_data.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.pracownik_data_CellEndEdit);
            // 
            // refresh_button
            // 
            this.refresh_button.Location = new System.Drawing.Point(12, 295);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(75, 23);
            this.refresh_button.TabIndex = 1;
            this.refresh_button.Text = "Odśwież";
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // deleteRecord
            // 
            this.deleteRecord.Enabled = false;
            this.deleteRecord.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deleteRecord.Location = new System.Drawing.Point(94, 294);
            this.deleteRecord.Name = "deleteRecord";
            this.deleteRecord.Size = new System.Drawing.Size(75, 23);
            this.deleteRecord.TabIndex = 2;
            this.deleteRecord.Text = "Usuń";
            this.deleteRecord.UseVisualStyleBackColor = true;
            this.deleteRecord.Visible = false;
            this.deleteRecord.Click += new System.EventHandler(this.deleteRecord_Click);
            // 
            // Dane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.deleteRecord);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.pracownik_data);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dane";
            this.Text = "Dane";
            this.Load += new System.EventHandler(this.Dane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pracownik_data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView pracownik_data;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.Button deleteRecord;
    }
}