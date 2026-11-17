using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursovay.Factory
{
    /// <summary>
    /// интерфейс фабрики окон, позволяет упростить создание окон от остальной логики приложения
    /// </summary>
    internal interface IWindowFactory
    {
        public Window CreateWindowForUser(Models.User user);
    }
}
