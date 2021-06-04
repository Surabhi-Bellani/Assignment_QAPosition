Feature: AddingSelectItemInCart

Background:
	Given User is on Amazon home page
	And Location is selected as US

@mytag
Scenario Outline: Adding Selected item into cart
	Given 'All Departments' option is selected from the dropdown
	And User Enters <ItemName> in the Search box
	When User clicks on Search
	And User clicks on the product link
	Then User Clicks on 'Add To Cart'
	And Item is moved to the Cart

	Examples:
		| ItemName |
		| ItemName |

#Data scraping functionality

Scenario: Print All the options Displayed after Searching for an Item and Selecting the filter
	Given 'All Departments' option is selected from the dropdown
	And User Enters <ItemName> in the Search box
	When User clicks on Search
	And Select the Price Filter
	Then User sees all the options presented for that item
	And User Wants to Save all the data in Txt file

	Examples:
		| ItemName |
		| ItemName |