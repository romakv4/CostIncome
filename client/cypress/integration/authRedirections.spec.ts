/// <reference types="Cypress"/>

context("Auth redirections", () => {
    describe("Go to page with redirect", () => {
        
        beforeEach(() => {
            cy.window().then((win) => {
                win.sessionStorage.clear();
            });
            cy.reload();
            cy.visit('/authorization');
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('password');
            cy.get('form').submit();
        })

        it("Try to go to authorization page if token not expired", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
            });
            cy.go('back');
            cy.location('pathname').should('eq', '/home');
            cy.get('button').contains('Log out').click();
        })

        it("Try to go to authorization page if token is manually deleted", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
                sessionStorage.clear();
            });
            cy.go('back');
            cy.location('pathname').should('eq', '/authorization');
        })

        it("Reload home page if token is manually deleted", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
                sessionStorage.clear();
            });
            cy.reload();
            cy.location('pathname').should('eq', '/authorization');
        })

        it("Try to go to registration if token not expired", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
            });
            cy.visit('/registration');
            cy.location('pathname').should('eq', '/home');
        })

        it("Try to go to registration if token is manually deleted", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
                sessionStorage.clear();
            });
            cy.visit('/registration');
            cy.location('pathname').should('eq', '/registration');
        })

        it("Try to go to main page if token is not expired", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
            });
            cy.visit('/');
            cy.location('pathname').should('eq', '/home');
        })

        it("Try to go to main page if token is manually deleted", () => {
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.a('string');
                sessionStorage.clear();
            });
            cy.visit('/');
            cy.location('pathname').should('eq', '/');
        })
    })
})