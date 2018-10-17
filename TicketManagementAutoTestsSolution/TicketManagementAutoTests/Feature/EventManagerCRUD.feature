Feature: EventManagerCRUD
	As a manager
	I can to logged in the site
	And I can create, update and delete event

@DeleteEventAndLogoutAfter
Scenario: Login like a manager and create new event
	Given User is logged in to the system as manager
	When Click MenuDropDown button
	And Click EventMenu button
	And Click CreateEvent button
	And Enter "testEvent" to Name input
	And Enter "test description" to Description input
	And Enter "11/07/2022 15:00" to DateTimeStart input
	And Enter "11/07/2022 18:00" to DateTimeFinish input
	And Enter "http://bipbap.ru/wp-content/uploads/2017/06/tmb_145037_6611.jpg" to ImageUrl input
	And Choose "Layout 2" in LayoutName dropdown menu
	And Click Create button
	And Enter "5" to Price input
	And Click Save button
	Then Event with name "testEvent" appeared in system

@LoggedInAsManagerAndCreateNewEventBefore
@DeleteEventAndLogoutAfter
Scenario: Update new event
	Given Click Edit button on new event
	When Enter "updatedEvent" to Name input
	And Enter "updated description" to Description input
	And Enter "11/02/2022 15:00" to DateTimeStart input
	And Enter "11/02/2022 18:00" to DateTimeFinish input
	And Enter "https://s00.yaplakal.com/pics/pics_original/9/2/3/8037329.jpg" to ImageUrl input
	And Choose "Layout 2" in LayoutName dropdown menu
	And Click Create button
	Then Event with name "updatedEvent" appeared in system

@LoggedInAsManagerAndCreateNewEventBefore
@LogoutAfter
Scenario: Delete new event
	Given Click Delete button on new event
	When Click OK on confirm form
	Then Event with name "test" is not present in system more