using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Tests.DomainModels
{
    [TestClass]
    public class AgendaValidationTests
    {
        [TestMethod]
        public void CanCreateAgendaItem()
        {
            var guid = Guid.NewGuid();
            try
            {
                var agendaItem = new AgendaModel(
                    guid,
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    "Agenda Item",
                    "Agenda Item Description",
                    "0"
                );
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
        
        [TestMethod]
        public void ThrowsWithTitle()
        {
            var guid = Guid.NewGuid();

            Assert.ThrowsException<ValidationException>(() => {
                var agendaItem = new AgendaModel(
                    guid,
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    "",
                    "Agenda Item Description",
                    "0"
                );
            });
        }
        
        [TestMethod]
        public void ThrowsWithDescription()
        {
            var guid = Guid.NewGuid();

            Assert.ThrowsException<ValidationException>(() => {
                var agendaItem = new AgendaModel(
                    guid,
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    "Agenda Item",
                    "",
                    "0"
                );
            });
        }
    }
}