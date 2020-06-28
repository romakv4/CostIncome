/// <reference types="Cypress"/>

context("Delete cost", () => {

    beforeEach(() => {
        cy.window().then((win) => {
            win.sessionStorage.clear();
        });
        cy.reload();
    })

    it("Delete one cost", () => {
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
        cy.get('input#price').type('100');
        cy.get('input#date').type('2020-06-25');
        cy.get('form').submit();
        cy.get('span').contains('Success! The form has been reset.').should('be.visible');
        cy.get('[data-cy="back"]').click();
        cy.location('pathname').should('eq', '/costs');
        cy.get('[data-cy="delete"]').should('be.visible').click();
        cy.location('pathname').should('eq', '/home');
        cy.get('[data-cy="add-cost"]').should('be.visible');
    })

    it("Delete multiple costs", () => {
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

        cy.get('input#category').type('Alcohol');
        cy.get('input#price').type('{selectall}100');
        cy.get('input#date').type('2020-06-25');
        cy.get('form').submit();
        cy.get('span').contains('Success! The form has been reset.').should('be.visible');

        cy.get('input#category').type('Tools');
        cy.get('input#description').type('Screwdriver');
        cy.get('input#price').type('{selectall}100');
        cy.get('input#date').type('2020-06-25');
        cy.get('form').submit();
        cy.get('span').contains('Success! The form has been reset.').should('be.visible');
        
        cy.get('[data-cy="back"]').click();
        cy.location('pathname').should('eq', '/costs');

        cy.get('i[data-cy="delete"]').each((elem, index, list) => {
            elem.click();
        });
    })

})