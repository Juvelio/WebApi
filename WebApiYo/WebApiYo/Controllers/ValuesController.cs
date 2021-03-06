﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiYo.Data;
using WebApiYo.Models;
using WebApiYo.Services;

namespace WebApiYo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ValuesController : ControllerBase
    {
        private readonly IDataProtector _protector;
        private readonly HashService _hashService;

        //==================PROCEDIMIENTO ALMACENADO
        private readonly ValuesRepository _repository;


        //===============================
        //private readonly ValuesRepository _repository;

        //public ValuesController(ValuesRepository repository)
        //{
        //    this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        //}
        //===============================

        public ValuesController(IDataProtectionProvider protectionProvider, HashService hashService, ValuesRepository repository)
        {
            _protector = protectionProvider.CreateProtector("valor_unico_y_quizas_secreto");
            _hashService = hashService;

            //==================PROCEDIMIENTO ALMACENADO
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        // GET api/values
        [HttpGet]
        [EnableCors("PermitirApiRequest")]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("hash")]
        public ActionResult GetHash()
        {
            string textoPlano = "Felipe Gavilán";
            var hashResult1 = _hashService.Hash(textoPlano).Hash;
            var hashResult2 = _hashService.Hash(textoPlano).Hash;
            return Ok(new { textoPlano, hashResult1, hashResult2 });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            string textoPlano = "Felipe Gavilán";
            string textoCifrado = _protector.Protect(textoPlano);
            string textoDesencriptado = _protector.Unprotect(textoCifrado);
            return Ok(new { textoPlano, textoCifrado, textoDesencriptado });
        }

        private void EjemploDeEncriptacionLimitadaPorTiempo()
        {
            var protectorLimitadoPorTiempo = _protector.ToTimeLimitedDataProtector();
            string textoPlano = "Felipe Gavilán";
            string textoCifrado = protectorLimitadoPorTiempo.Protect(textoPlano, TimeSpan.FromSeconds(5));
            string textoDesencriptado = protectorLimitadoPorTiempo.Unprotect(textoCifrado);
        }

        // POST api/values
        //[HttpPost]
        //public ActionResult Post([FromBody] string value)
        //{
        //    return Ok();
        //}

        //==================PROCEDIMIENTO ALMACENADO
        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Value value)
        {
            await _repository.Insert(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        //==================PROCEDIMIENTO ALMACENADO
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
