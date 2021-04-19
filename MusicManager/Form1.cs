using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MusicManager
{
    public partial class Form1 : Form
    {

        private string connectionString = "server=LAB35\\SQL2012;database=MusicManager;uid=lab;pwd=";
        private DataTable dtAvailableSongs = new DataTable();
        private DataTable dtSelectedSongs = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.loadArtist();
            this.loadSelectedSongs();
        }

        private void loadArtist()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string queryString = "SELECT * FROM Artists";
                SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboArtist.DataSource = dt;
                cboArtist.DisplayMember = "ArtistName";
                cboArtist.ValueMember = "ArtistID";
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void loadAvailableSongs()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                string queryString = "SELECT SongID, (CONVERT(NVARCHAR(4), SongID) + ' - ' + SongName) AS SongName FROM Songs WHERE ArtistID = " + cboArtist.SelectedValue;
                SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
                dtAvailableSongs = new DataTable();
                da.Fill(dtAvailableSongs);
                lstAvailableSongs.DataSource = dtAvailableSongs;
                lstAvailableSongs.DisplayMember = "SongName";
                lstAvailableSongs.ValueMember = "SongID";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void loadSelectedSongs()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                string queryString = "SELECT SongID, (CONVERT(NVARCHAR(4), SongID) + ' - ' + SongName) AS SongName FROM Songs WHERE 1 > 2";
                SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
                dtSelectedSongs = new DataTable();
                da.Fill(dtSelectedSongs);
                lstSelectedSongs.DataSource = dtSelectedSongs;
                lstSelectedSongs.DisplayMember = "SongName";
                lstSelectedSongs.ValueMember = "SongID";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cboArtist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.loadAvailableSongs();
        }

        private void cboArtist_SelectedValueChanged(object sender, EventArgs e)
        {
            this.loadAvailableSongs();
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            if (dtAvailableSongs.Rows.Count > 0)
            {
                DataRow dataRow = dtSelectedSongs.NewRow();
                dataRow["SongID"] = lstAvailableSongs.SelectedValue;
                dataRow["SongName"] = lstAvailableSongs.Text;
                dtSelectedSongs.Rows.Add(dataRow);
                dtSelectedSongs.AcceptChanges();

                foreach (DataRow dr in dtAvailableSongs.Rows)
                {
                    if (dr["SongID"].ToString() == dataRow["SongID"].ToString())
                    {
                        dr.Delete();
                    }
                }
                dtAvailableSongs.AcceptChanges();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dtSelectedSongs.Rows.Count > 0)
            {
                DataRow dataRow = dtAvailableSongs.NewRow();
                dataRow["SongID"] = lstSelectedSongs.SelectedValue;
                dataRow["SongName"] = lstSelectedSongs.Text;
                dtAvailableSongs.Rows.Add(dataRow);
                dtAvailableSongs.AcceptChanges();

                foreach (DataRow dr in dtSelectedSongs.Rows)
                {
                    if (dr["SongID"].ToString() == dataRow["SongID"].ToString())
                    {
                        dr.Delete();
                    }
                }
                dtSelectedSongs.AcceptChanges();
            }
        }
    }
}
