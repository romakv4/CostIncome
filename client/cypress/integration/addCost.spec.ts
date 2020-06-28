/// <reference types="Cypress"/>

context("Add new cost", () => {

    beforeEach(() => {
        cy.window().then((win) => {
            win.sessionStorage.clear();
        });
        cy.reload();
    })

    describe("Form validation check", () => {
        it("Submit without any entries", () => {
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
            cy.get('form').submit();
            cy.get('input#category').should('have.class', 'erroredFormField');
            cy.get('span').contains('Category is required');
        })

        it("Drop default values and try to submit", () => {
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
            cy.get('input#price').type('{selectall}{del}');
            cy.get('input#date').type('{selectall}{del}');
            cy.get('form').submit();
            cy.get('input#category').should('have.class', 'erroredFormField');
            cy.get('span').contains('Category is required');
            cy.get('input#description').should('have.class', 'validFormField');
            cy.get('input#price').should('have.class', 'erroredFormField');
            cy.get('span').contains('Price is required');
            cy.get('input#date').should('have.class', 'erroredFormField');
            cy.get('span').contains('Date is required');
        })

        it("Category and description contains more than 20 characters and price value more than 999999999999", () => {
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
            cy.get('input#category').type('internationalizzation');
            cy.get('input#description').type('llooccaallizzattiioonn');
            cy.get('input#price').type('{selectall}1000000000000');
            cy.get('input#date').type('2020-06-25');
            cy.get('form').submit();
            cy.get('input#category').should('have.class', 'erroredFormField');
            cy.get('span').contains('Category must be shorter than 20 characters');
            cy.get('input#description').should('have.class', 'erroredFormField');
            cy.get('span').contains('Description must be shorter than 20 characters');
            cy.get('input#price').should('have.class', 'erroredFormField');
            cy.get('span').contains('Are you serious?');
        })

        it("Valid category and description and price value less than 0.01", () => {
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
            cy.get('input#category').type('internationalizzatio');
            cy.get('input#description').type('llooccaallizzattiion');
            cy.get('input#price').type('{selectall}0.009');
            cy.get('input#date').type('2020-06-25');
            cy.get('form').submit();
            cy.get('input#price').should('have.class', 'erroredFormField');
            cy.get('span').contains('Price must be greater than 0.01');
        })
    })

    describe("If the user has no cost records", () => {
        it("Valid case", () => {
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
            cy.get('input#category').type('test category');
            cy.get('input#description').type('test description');
            cy.get('input#price').type('{selectall}100');
            cy.get('input#date').type('2020-06-25');
            cy.get('form').submit()
            cy.get('span').contains('Success! The form has been reset.').should('be.visible');
            cy.get('[data-cy="back"]').click();
            cy.location('pathname').should('eq', '/costs');
            cy.get('td').contains('test category').should('be.visible');
            cy.get('td').contains('test description').should('be.visible');
            cy.get('td').contains('100').should('be.visible');
            cy.get('td').contains('2020.06.25').should('be.visible');
            cy.get('[data-cy="edit"]').should('be.visible');
            cy.get('[data-cy="delete"]').should('be.visible').click();
            cy.location('pathname').should('eq', '/home');
            cy.get('[data-cy="add-cost"]').should('be.visible');
            cy.reload();
        })
    })
})