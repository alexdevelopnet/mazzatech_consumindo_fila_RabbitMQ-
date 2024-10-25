using Microsoft.AspNetCore.Mvc;
using Publisher.Data;
using Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Protocolo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProtocoloController : ControllerBase
    {
        private readonly ProtocoloContext _context;

        public ProtocoloController(ProtocoloContext context)
        {
            _context = context;
        }

        [HttpGet("{numeroProtocolo}")]
        public ActionResult<Entity.Protocolo> GetProtocoloPorNumero(string numeroProtocolo)
        {
            var protocolo = _context.Protocolos.SingleOrDefault(p => p.NumeroProtocolo == numeroProtocolo);
            if (protocolo == null)
                return NotFound();

            return protocolo;
        }

        [HttpGet("cpf/{cpf}")]
        public ActionResult<IEnumerable<Entity.Protocolo>> GetProtocolosPorCpf(string cpf)
        {
            var protocolos = _context.Protocolos.Where(p => p.Cpf == cpf).ToList();
            return protocolos;
        }

        [HttpGet("rg/{rg}")]
        public ActionResult<IEnumerable<Entity.Protocolo>> GetProtocolosPorRg(string rg)
        {
            var protocolos = _context.Protocolos.Where(p => p.Rg == rg).ToList();
            return protocolos;
        }

         
    }

}
