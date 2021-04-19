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
    public partial class FormMain : Form
    {

        private string connectionString = "server=LAB35\\SQL2012;database=MusicManager;uid=lab;pwd=";
        private DataTable dtAvailableSongs = new DataTable();
        private DataTable dtSelectedSongs = new DataTable();

        public FormMain()
        {
            InitializeComponent();
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

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string queryString = "INSERT INTO Albums (AlbumName, CreatedDate) VALUES (N'" + txtAlbumName.Text.Replace("'", "''") + "', GETDATE())";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = queryString;
                cmd.CommandType = CommandType.Text;
                Int32 recordAffected = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter("SELECT MAX(AlbumID) FROM Albums", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string AlbumID = dt.Rows[0][0].ToString();

                foreach (DataRow dr in dtSelectedSongs.Rows)
                {
                    queryString = "INSERT INTO AlbumSong (AlbumID, SongID) VALUES (" + AlbumID + ", " + dr["SongID"] + ")";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Record added successfully.");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
