using ScarpeCommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ScarpeCommerce.Controllers
{
    public class ScarpeController : Controller
    {
        // GET: Scarpe
        public ActionResult Index()
        {
            List<Scarpe> scarpe = new List<Scarpe>();
            string scarpeDB = ConfigurationManager.ConnectionStrings["ScarpeDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(scarpeDB);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Scarpe";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Scarpe s = new Scarpe();
                    s.ID = Convert.ToInt32(reader["IDScarpe"]);
                    s.Nome = reader["Nome"].ToString();
                    s.Descrizione = reader["Descrizione"].ToString();
                    s.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    s.ImmagineCopertina = reader["ImgCopertina"].ToString();
                    s.ImmagineAggiuntiva1 = reader["ImmagineAggiuntiva1"].ToString();
                    s.ImmagineAggiuntiva2 = reader["ImmagineAggiuntiva2"].ToString();
                    scarpe.Add(s);

                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Errore: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(scarpe);
        }
        public ActionResult Dettagli(string id)
        {
            Scarpe s = new Scarpe();
            string scarpeDB = ConfigurationManager.ConnectionStrings["ScarpeDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(scarpeDB);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Scarpe WHERE Nome = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    s.ID = Convert.ToInt32(reader["IDScarpe"]);
                    s.Nome = reader["Nome"].ToString();
                    s.Descrizione = reader["Descrizione"].ToString();
                    s.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    s.ImmagineCopertina = reader["ImgCopertina"].ToString();
                    s.ImmagineAggiuntiva1 = reader["ImmagineAggiuntiva1"].ToString();
                    s.ImmagineAggiuntiva2 = reader["ImmagineAggiuntiva2"].ToString();
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Errore: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(s);
        }
    }
}