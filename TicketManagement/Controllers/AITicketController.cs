using BusinessLogic.Entities;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TicketManagement.Helpers;
using TicketManagement.Models;
using TicketManagement.Services;

namespace TicketManagement.Controllers
{
    public class AITicketController : Controller
    {
        private readonly OpenAIService _openAiService;
        private readonly ITicketRepository _ticketRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly TicketHelper _ticketHelper;
        private readonly IPriorityRepository _priorityRepository;
        private readonly IUserRepository userRepository;
        public AITicketController(OpenAIService openAiService,
                                  ITicketRepository ticketRepository,
                                  IStatusRepository statusRepository,
                                  TicketHelper ticketHelper,
                                  ICategoryRepository categoryRepository,
                                  IPriorityRepository priorityRepository,
                                  IUserRepository userRepository)
        {
            _openAiService = openAiService;
            _ticketRepository = ticketRepository;
            _statusRepository = statusRepository;
            _ticketHelper = ticketHelper;
            _categoryRepository = categoryRepository;
            _priorityRepository = priorityRepository;
            this.userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GenerateViaAjax([FromBody] AIPromptModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserPrompt))
                return Json(new { success = false, message = "Promptul este gol." });

            var aiResponse = await _openAiService.GenerateTicketAsync(model.UserPrompt);
            if (string.IsNullOrWhiteSpace(aiResponse))
                return Json(new { success = false, message = "AI-ul nu a generat răspuns." });

            try
            {
                var ticket = JsonSerializer.Deserialize<TicketEntity>(aiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                var statusToDo = _statusRepository.GetStatusById(1004);
                if (statusToDo is not null)
                {
                    ticket.StatusEntity = _ticketHelper.ToBusinessStatusEntity(statusToDo);
                }
                var user = userRepository.GetUserByEmail(HttpContext.User.Identity.Name);
                var email = HttpContext.User.Identity.Name;
                var category = _categoryRepository.Get(8);
                var priority = _priorityRepository.Get(1);
                var ticketData = _ticketHelper.ToDataAccessTicket(ticket, email, category, statusToDo, priority, user);
                if (ticket != null)
                {
                    _ticketRepository.InsertTicket(ticketData);
                    _ticketRepository.Save();
                }
                return Json(new { success = true, message = "Tichet creat cu succes!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Răspuns invalid de la AI." });
            }
        }
    }
}
