// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

// tslint:disable-next-line: no-namespace
declare namespace Cypress {
    interface Chainable {
        signIn(email: string, password: string);
        clearSessionStorage();
    }
}

Cypress.Commands.add('signIn', (email: string, password: string) => {
    cy.visit('/authorization');
    cy.get('input#email').type(email);
    cy.get('input#password').type(password);
    cy.get('form').submit();
    cy.location('pathname').should('eq', '/home').should(() => {
        expect(
            sessionStorage.getItem('token')
        ).to.be.a('string');
    });
});

Cypress.Commands.add('clearSessionStorage', () => {
    cy.window().then((win) => {
        win.sessionStorage.clear();
    });
});
