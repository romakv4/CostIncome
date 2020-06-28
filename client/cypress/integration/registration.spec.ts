/// <reference types="Cypress"/>

context("Registration", () => {
    describe("Registration with invalid data", () => {

        beforeEach(() => {
            cy.window().then((win) => {
                win.sessionStorage.clear();
            });
            cy.reload();
            cy.visit("/registration");
        })

        it("form has autocomplete attribute", () => {
            cy.get('form').should('have.attr', 'autocomplete', 'off');
        })

        it("submit without any entries", () => {
            cy.get('form').submit();
            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('Email is required').should('be.visible');
            cy.get('input#password').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password is required').should('be.visible');
            cy.get('input#confirmPassword').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password repeat is required').should('be.visible');
        })

        it("submit with invalid email", () => {
            cy.get('input#email').type('costincometestuser');
            cy.get('form').submit();
            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('Invalid e-mail').should('be.visible');
        })

        it("submit with short password", () => {
            cy.get('input#password').type('passwor');
            cy.get('form').submit();
            cy.get('input#password').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password must be longer than 8 characters').should('be.visible');
        })

        it("submit with non equal passwords", () => {
            cy.get('input#password').type('passwor');
            cy.get('input#confirmPassword').type('password');
            cy.get('form').submit();
            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('Email is required').should('be.visible');
            cy.get('input#password').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password must be longer than 8 characters').should('be.visible');
            cy.get('input#confirmPassword').should('have.class', 'erroredFormField');
            cy.get('span').contains("Doesn't match password").should('be.visible');
        })

        it("submit with valid email and non equal passwords", () => {
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('passwor');
            cy.get('input#confirmPassword').type('password');
            cy.get('form').submit();
            cy.get('input#password').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password must be longer than 8 characters').should('be.visible');
            cy.get('input#confirmPassword').should('have.class', 'erroredFormField');
            cy.get('span').contains('Doesn\'t match password').should('be.visible');
        })

        it("submit with already existing email", () => {
            cy.server();
            cy.route({
                method: 'POST',
                url: 'api/auth/register'
            }).as('register');

            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('password');
            cy.get('input#confirmPassword').type('password');
            cy.get('form').submit();

            cy.wait('@register');

            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('Email already exists').should('be.visible');
        })  
    })
})