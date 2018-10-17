Feature: LoginForUser
	As a user
	I want to be able to login site
	So I can do it from login page

Scenario: Login to localhost is successful with right credentials
	Given User is on localhost
	When User clicks Login button
	And Enters "user" to user input
	And Enters "user" to password input
	And Clicks Enter button
	Then Welcome line has message "Hello, user!"

Scenario: Login to localhost is unsuccessful with wrong password
	Given User is on localhost
	When User clicks Login button
	And Enters "user" to user input
	And Enters "1111" to password input
	And Clicks Enter button
	Then Login page has error "Error"
