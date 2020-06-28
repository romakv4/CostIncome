/// <reference types="Cypress"/>

context("Edit cost", () => {

    beforeEach(() => {
        cy.window().then((win) => {
            win.sessionStorage.clear();
        });
        cy.reload();
    })

    it("Edit one cost", () => {
        cy.visit('/authorization');
        cy.get('input#email').type('costincometestuser@gmail.com');
        cy.get('input#password').type('password');
        cy.get('form').submit();
        cy.location('pathname').should('eq', '/home').should(() => {
            expect(
                sessionStorage.getItem('token')
            ).to.be.a('string');
        });
        cy.get('[data-cy="add-cost"]').should('be.visible').click();
        cy.get('input#category').type('Food');
        cy.get('input#price').type('{selectall}100');
        cy.get('input#date').type('2020-06-25');
        cy.get('form').submit();
        cy.get('span').contains('Success! The form has been reset.').should('be.visible');
        cy.get('[data-cy="back"]').click();
        cy.location('pathname').should('eq', '/costs');
        cy.get('[data-cy="edit"]').should('be.visible').click();
        cy.location('pathname').should('contain', '/edit-cost');
        cy.get('input#category').type('{selectall}Alcohol')
        cy.get('input#description').type('{selectall}Very good wine')
        cy.get('input#price').type('{selectall}1000')
        cy.get('input#date').type('2020-06-13');
        cy.get('form').submit();
        cy.location('pathname').should('eq', '/costs');
        cy.get('td').contains('Alcohol').should('be.visible');
        cy.get('td').contains('Very good wine').should('be.visible');
        cy.get('td').contains('1000').should('be.visible');
        cy.get('td').contains('2020.06.13').should('be.visible');
        cy.get('[data-cy="delete"]').click();
    })

})