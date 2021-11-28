using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SinSeguridad : ISeguridad
    {
		public byte[] EncriptarPass(string pass)
		{
			return Encoding.UTF8.GetBytes(pass);
		}

		public bool ValidarPass(string pass)
		{
			return true;
		}
	}
}
