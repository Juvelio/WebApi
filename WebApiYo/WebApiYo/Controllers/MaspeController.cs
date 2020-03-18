using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYo.Data;
using WebApiYo.Models;

namespace WebApiYo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class MaspeController : ControllerBase
    {
        private readonly IDataProtector _protector;
        private readonly MaspeRepository _repository;


        public MaspeController(IDataProtectionProvider protectionProvider, MaspeRepository repository)
        {
            _protector = protectionProvider.CreateProtector("valor_unico_y_quizas_secreto");
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //[HttpGet]
        [HttpGet("Documento")]
        [EnableCors("PermitirApiRequest")]
        public async Task<ActionResult<IEnumerable<Maspe>>> Get(string Cip, string Dni)
        {
            return await _repository.ListarPersonal(Cip, "", "", "", Dni);
        }


        //[HttpGet("Documento")]
        //public async Task<ActionResult<IEnumerable<Maspe>>> Get(string Cip, string Dni)
        //{
        //    List<Maspe> Lista = null;
        //    try
        //    {
        //        Lista = await MaspeRepository.getInstance().ListarPersonal(Cip, "", "", "", Dni);
        //        //Lista = await MaspeRepository.getInstance().ListarPersonalDinamico(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch, Cip, Paterno, Materno, Nombre, Dni);

        //    }
        //    catch (Exception ex)
        //    {
        //        Lista = new List<Maspe>();
        //    }
        //    return Lista;
        //}
    }
}
