using AutoFixture;
using Moq;
using ServiceContracts;
using CRUDExample.Controllers;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CRUDTests
{
    public class PersonsControllerTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly Mock<IPersonsService> _personsServiceMock;
        private readonly Mock<ICountriesService> _countriesServiceMock;
        private readonly Fixture _fixture;

        public PersonsControllerTest()
        {
            _fixture = new Fixture();
            _countriesServiceMock = new Mock<ICountriesService>();
            _personsServiceMock = new Mock<IPersonsService>();

            _countriesService = _countriesServiceMock.Object;
            _personsService = _personsServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithPersonList()
        {
            List<PersonResponse> person_responses_list = _fixture.Create<List<PersonResponse>>();

            PersonsController personsController = new PersonsController(_personsService, _countriesService);

            _personsServiceMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(person_responses_list);

            _personsServiceMock.Setup(temp => temp.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrderOptions>())).ReturnsAsync(person_responses_list);

            IActionResult actionResult = await personsController.Index(_fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<SortOrderOptions>());


            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

            Assert.IsAssignableFrom<IEnumerable<PersonResponse>>(viewResult.ViewData.Model);
            Assert.Equal(person_responses_list, viewResult.ViewData.Model);
        }
        #endregion

        #region Create

        [Fact]
        public async Task Create_IfModelErrors_ToReturnCreateView()
        {
            PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();

            PersonResponse person_response = _fixture.Create<PersonResponse>();

            List<CountryResponse> countries =_fixture.Create<List<CountryResponse>>();

            _countriesServiceMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countries);

            _personsServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest>())).ReturnsAsync(person_response);

            PersonsController personsController = new PersonsController(_personsService, _countriesService);

            personsController.ModelState.AddModelError("PersonName", "Person Name can't be blank");

            IActionResult actionResult = await personsController.Create(person_add_request);
              
            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

            Assert.IsAssignableFrom<PersonAddRequest>(viewResult.ViewData.Model);
            Assert.Equal(person_add_request, viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            PersonAddRequest person_add_request = _fixture.Create<PersonAddRequest>();
            PersonResponse person_response = _fixture.Create<PersonResponse>();
            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

            _countriesServiceMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(countries);
            _personsServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest>())).ReturnsAsync(person_response);

            PersonsController personsController = new PersonsController(_personsService, _countriesService);

            IActionResult actionResult = await personsController.Create(person_add_request);

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(actionResult);

            Assert.True(redirectResult.ActionName == "Index");
        }
        #endregion

    }
}