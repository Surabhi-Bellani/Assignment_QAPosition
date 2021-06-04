Feature: CreateAccountForNewUser

Background:
	Given User is on Amazon home page


Scenario: Create Account with Duplicate email
	Given User hover SignIn option
	And Click on Start here
	And User is navigated to User Creation Page
	When User enters valid 'UserName','Email','Password','ReEnterPassword'
	And User Click on Create your Amazon Account
	Then error message is displayed