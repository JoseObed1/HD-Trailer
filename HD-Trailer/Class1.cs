using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HD_Trailer
{
    public class CodigoDB
    {
        DBEntity db = new DBEntity();

        #region Cuentas
        public void VisualizarDatos(string id)
        {
            usuarios cuentas = db.usuarios.Find(id);
            System.Diagnostics.Debug.WriteLine(cuentas.username.ToString());
        }

        public void NuevoUsuario(usuarios datos)
        {
            db.usuarios.Add(datos);
            db.SaveChanges();
        }

        public void EditarUsuario(usuarios elemento)
        {
            db.Entry(elemento).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarUsuario(string id)
        {
            usuarios tempo = db.usuarios.Find(id);
            db.usuarios.Remove(tempo);
            db.SaveChanges();
        }

        /*
        public bool ValidarLogin(string correo, string contra)
        {
            var existe = db.Cuentas.FirstOrDefault(tempolog => tempolog.Correo == correo && tempolog.Password == contra);

            if (existe != null)
            {
                System.Diagnostics.Debug.WriteLine("X-potato!");
                return true;
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("Sad Programmer Noises");
                return false;
            }
        }*/
        #endregion


        #region Foros
        public void NuevoVideo(youtube datos)
        {
            db.youtube.Add(datos);
            db.SaveChanges();
        }
        #endregion
    }
}