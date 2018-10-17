Feature: BuyATicket
	User can logged in as Manager and as User
	And buy a ticket

@LoggedInAsManagerAndCreateEventForAutotestAndLoggedOutBefore
@DeleteEventDeleteOrderReturnMoneyAfter
@LogoutAfter
Scenario: Manager buy a ticket
	Given User logged in as a manager
	When User chose and clicks For Autotest event
	And User chose and clicks Add to cart on first seat
	And User clicks Buy button
	Then User see his ticket in his cart

@LoggedInAsManagerAndCreateEventForAutotestAndLoggedOutBefore
@DeleteEventDeleteOrderReturnMoneyAfter
@LogoutAfter
Scenario: User buy a ticket
	Given User logged in as a user
	When User chose and clicks For Autotest event
	And User chose and clicks Add to cart on first seat
	And User clicks Buy button
	Then User see his ticket in his cart