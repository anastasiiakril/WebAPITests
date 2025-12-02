Feature: CRUD operations 



Background:
    Given the API is initialized

  Scenario: CRUD
    When I create a booking
    Then the booking is created successfully

    When I get the booking
    Then the booking is retrieved successfully

    When I update the booking
    Then the booking is updated successfully

    When I delete the booking
    Then the booking is deleted successfully