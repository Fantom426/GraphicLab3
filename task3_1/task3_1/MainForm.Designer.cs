using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace task3_1
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Choose color or file for fill";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Color";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Click1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(13, 70);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "File";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Click2);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(143, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(252, 236);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_down);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_move);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouse_up);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(13, 100);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(104, 24);
			this.radioButton1.TabIndex = 5;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Draw";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(13, 131);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(104, 24);
			this.radioButton2.TabIndex = 6;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Fill";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(405, 261);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "task3_1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;

		void Click1(object sender, System.EventArgs e){
			colorDialog1.ShowDialog();
			choose = 'c';
		}
		
		void Click2(object sender, System.EventArgs e){
			openFileDialog1.ShowDialog();
			choose = 'f';
			pic = new Bitmap(openFileDialog1.FileName);
		}

		void mouse_down(object sender, System.Windows.Forms.MouseEventArgs e){
			if(radioButton1.Checked){
				prev = new System.Drawing.Point(e.X,e.Y);
				can_draw = true;
			}
			else{
				Graphics g = Graphics.FromImage(bmp);
				if (choose == 'c')
					fill_col(new Point(e.X, e.Y), bmp.GetPixel(e.X,e.Y), g);
				else
					fill_pic(new Point(e.X, e.Y), bmp.GetPixel(e.X,e.Y), g);
				g.Dispose();
				pictureBox1.Image=bmp;
			}
			
		}

		void mouse_move(object sender, System.Windows.Forms.MouseEventArgs e){
			if (!can_draw) return;
			pictureBox1.Image = bmp;
			Graphics g = Graphics.FromImage(bmp);
			Point p = new Point(e.X,e.Y);
			Pen pen = new Pen(Color.Black);
			g.DrawLine(pen, prev,p);
			prev = p;
			g.Dispose();
			pictureBox1.Invalidate();
		}
		
		void mouse_up(object sender, System.Windows.Forms.MouseEventArgs e){
			can_draw = false;
		}
		
		void fill_col(Point p, Color c, Graphics g){
		
			if (bmp.GetPixel(p.X, p.Y) != c) return;
			if (bmp.GetPixel(p.X, p.Y) == colorDialog1.Color) return;
			
			Point left = get_left(p,c);
			Point right = get_right(p,c);
			
			Pen pen = new Pen(colorDialog1.Color);
			g.DrawLine(pen, left, right);
			pen.Dispose();
			
			if (p.Y>=bmp.Height-1) return;
			for(int i = left.X; i<=right.X; i++){
				fill_col(new Point(i,p.Y+1), c, g);
			}
			
			if (p.Y<=0) return;
			for(int i = left.X; i<=right.X; i++){
				fill_col(new Point(i,p.Y-1), c, g);
			}
		}
		
		void fill_pic(Point p, Color c, Graphics g){
			
			if (bmp.GetPixel(p.X,p.Y) != c) return;
			
			Point left = get_left(p,c);
			Point right = get_right(p,c);
			
			int x = p.X;
			int y = p.Y;
			for(int i = left.X; i<=right.X; i++){
				bmp.SetPixel(i,y,pic.GetPixel(i%pic.Width,y%pic.Height));
			}
			
			if (p.Y>=bmp.Height-1) return;
			for(int i = left.X; i<=right.X; i++){
				fill_pic(new Point(i,p.Y+1), c, g);
			}
			
			if (p.Y<=0) return;
			for(int i = left.X; i<=right.X; i++){
				fill_pic(new Point(i,p.Y-1), c, g);
			}
		}
		
		Point get_left(Point p, Color c){
			int x = p.X;
			int y = p.Y;
			while (x>0 && c == bmp.GetPixel(x,y)){
				x--;
			}
			return new Point(x+1,y);
		}
		
		Point get_right(Point p, Color c){
			int x = p.X;
			int y = p.Y;
			while (x<bmp.Width && c == bmp.GetPixel(x,y)){
				x++;
			}
			return new Point(x-1,y);
		}
	}
}
