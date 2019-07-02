using System;
using System.Collections.Generic;
using System.Net.Mail;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class EventApplication
    {
        IEventRepository eventRepository;

        public EventApplication(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }
        
        public Guid Insert(EventDTO eventDto)
        {
            eventDto.EventId = Guid.NewGuid();
            eventDto.Address.AddressId = Guid.NewGuid();
            var sEvent = EventAdapter.ToDomain(eventDto);
            Guid id = eventRepository.Insert(sEvent);

            SendMessage();
            
            return id;
        }

        public Guid Update(EventDTO eventDto)
        {
            var sEvent = EventAdapter.ToDomain(eventDto);
            return eventRepository.Update(sEvent);
        }

        public bool Delete(Guid id)
        {
            return eventRepository.Delete(id);
        }

        public bool SetVoluntaryToEvent(Guid voluntarayId, Guid eventId)
        {
            return eventRepository.SetVoluntaryToEvent(voluntarayId, eventId);
        }
        
        public EventDTO Get(Guid id)
        {
            var sEvent = eventRepository.Find(id);
            return EventAdapter.ToDTO(sEvent);
        }

        public List<EventDTO> GetAll()
        {
            List<EventDTO> eventsDto = new List<EventDTO>();
            var events = eventRepository.FindAll();
            foreach (Event cli in events)
            {
                eventsDto.Add(EventAdapter.ToDTO(cli));
            }
            return eventsDto;
        }
        
        
        public static void SendMessage()
        {

            try {
                //http://www.macoratti.net/10/12/c_email2.htm
                //cria uma mensagem
                MailMessage mail = new MailMessage();
                
                //define os endereços
                mail.From = new MailAddress("managersystemvoluntary@gmail.com");
                mail.To.Add("joaopedrokaspary@hotmail.com");

                //define o conteúdo
                mail.Subject = "Este é um simples ,muito simples email";
                mail.Body = "Este é o corpo do email";

                //envia a mensagem
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                
//                smtp.UseDefaultCredentials = true;
            
                smtp.Send(mail);
            }  
            catch (Exception ex) {
                Console.WriteLine("Except send message: {0}", ex.ToString() );			  
            }              
        }
        
    }
}