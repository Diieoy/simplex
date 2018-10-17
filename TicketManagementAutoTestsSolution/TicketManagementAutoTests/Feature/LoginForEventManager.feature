Feature: LoginForEventManager
	As a Manager
	I want to be able to login site
	So I can do it from login page

Scenario: Login to localhost is successful with right credentials
	Given User is on localhost
	When User clicks Login button
	And Enters "manager" to user input
	And Enters "manager" to password input
	And Clicks Enter button
	Then Welcome line has message "Hello, manager!"

Scenario: Login to localhost is unsuccessful with wrong password
	Given User is on localhost
	When User clicks Login button
	And Enters "manager" to user input
	And Enters "12345" to password input
	And Clicks Enter button
	Then Login page has error "Error"
