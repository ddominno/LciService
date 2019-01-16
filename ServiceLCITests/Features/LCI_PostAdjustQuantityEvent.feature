Feature: LCI_PostAdjustQuantityEvent

Scenario: adjust Quantity of a bin in inventory
	Given URL for API is 'dlcis/api/v1/lcis/trans/binadjustqty'
	And Create POST Request
	And Payload is '..\..\DataSources\AdjustQuantity.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content 

Scenario: adjust quantity where Bin Id not in inventory
	Given a bin Id that does not exist in inventory
	And Create POST Request
	And the Payload is set for adjusting a bin
	And URL for API is 'dlcis/api/v1/lcis/trans/binadjustqty'
	When the Request is executed
	Then Status Code is 'Created'
	And the response property 'Message' contains 'does not appear to be in inventory'


