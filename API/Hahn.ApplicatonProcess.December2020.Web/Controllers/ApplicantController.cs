using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicantService _applicantService;
        private readonly IMapper _mapper;

        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            this._applicantService = applicantService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData), StatusCodes.Status201Created)] // Create
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] ApplicantVM model)
        {
            try
            {
                var applicant = _mapper.Map<Applicant>(model);
                var createdapplicant = await _applicantService.CreateAsync(applicant);

                if (createdapplicant != null)
                {
                    return Created("", new ResponseData
                    {
                        id = createdapplicant.ID,
                        url = $"{ this.Request.Scheme }://{this.Request.Host}{this.Request.PathBase}{ Request.Path.Value}?id={ createdapplicant.ID }"
                    }); ;
                }
                else
                {
                    return BadRequest(new List<string> { "Failed Create Applicant" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "Failed Create Applicant" });
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([FromQuery] int id)
        {
            try
            {
                var applicant = await _applicantService.ReadSingleAsync(id, false);
                if (applicant != null)
                {
                    var response = _mapper.Map<ApplicantVM>(applicant);
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "Failed Fetching Applicant Data" });
            }
        }



        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromQuery] int id, [FromBody] ApplicantVM model)
        {
            try
            {
                var applicant = _mapper.Map<Applicant>(model);
                applicant.ID = id;
                var checkexist = await _applicantService.ReadSingleAsync(id, false);
                if (checkexist == null)
                    return NotFound();

                if (applicant != null)
                {
                    await _applicantService.UpdateAsync(applicant);
                    return Ok("Applicant Data Update Successful");
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "Unable to Update Applicant Data" });
            }
        }


        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var applicant = await _applicantService.ReadSingleAsync(id, false);
                if (applicant != null)
                {
                    var response = _mapper.Map<Applicant>(applicant);
                    await _applicantService.DeleteAsync(id);
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(new List<string> { "Failed to Delete Applicant Data" });
            }
        }

    }
}
