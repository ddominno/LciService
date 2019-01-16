Feature: LCI_PostBinRemoveInspection

	Scenario: POST Bin Remove Inspection
	Given URL for API is 'dlcis/api/v1/lcis/trans/bininspectionremove'
	And Create POST Request
	And Payload is '..\..\DataSources\bininspectionremove.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content

	Scenario: Remove an inspection with the wrong Bin Id 
	Given the event Id of an inspection
	And a bin Id unrelated to the inspection
	And Create POST Request
	And the Payload is set for PostBinRemoveInspection
	And URL for API is 'dlcis/api/v1/lcis/trans/bininspectionremove'
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'message' contains 'Bad request'

	Scenario: Remove a record with an EventType not Inspection 
	Given the event Id of a 'Issuance'
	And a bin Id related to the inspection
	And Create POST Request
	And the Payload is set for PostBinRemoveInspection
	And URL for API is 'dlcis/api/v1/lcis/trans/bininspectionremove'
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'message' contains 'Bad request'
	And the inspection record exists on the database
