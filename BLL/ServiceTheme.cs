using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class ServiceTheme : IServiceTheme
    {

        private readonly Context _manager;
        public ServiceTheme(Context manager)
        {
            _manager = manager;
        }

        public bool Add(Theme theme)
        {
            try
            {
                _manager.Themes.Add(theme);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Theme theme = _manager.Themes.Find(id);
                _manager.Themes.Remove(theme);
                _manager.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Theme> FindAll()
        {
            try
            {
                return _manager.Themes.ToList();

            }
            catch (Exception)
            {

                return null;
            }
        }

        public Theme FindById(int id)
        {
            try
            {
                return _manager.Themes.Include(x=> x.Films).FirstOrDefault(id);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Update(Theme theme)
        {
            try
            {
                Theme themetoupdate = _manager.Themes.Find(theme.Id);
                _manager.Entry(themetoupdate).CurrentValues.SetValues(theme);
                _manager.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
