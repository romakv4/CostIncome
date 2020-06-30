context("Change password", () => {
    before(() => {
        cy.clearSessionStorage();
        cy.reload();
        cy.signIn('costincometestuser@gmail.com', 'password');
    })

    describe("Change password with invalid data", () => {

        beforeEach(() => {
            cy.visit('/changepassword');
        })

        it("Form has autocomplete attribute", () => {
            cy.get('form').should('have.attr', 'autocomplete', 'off');
        })

        it("Submit without any entries", () => {
            cy.get('form').submit();
            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('Email is required').should('be.visible');
            cy.get('input#password').should('have.class', 'erroredFormField');
            cy.get('span').contains('Password is required').should('be.visible');
            cy.get('input#newPassword').should('have.class', 'erroredFormField');
            cy.get('span').contains('New password is required').should('be.visible');
        })

        it("Submit with not existing email", () => {
            cy.server();
            cy.route({
                method: 'POST',
                url: 'api/auth/changepassword'
            }).as('changePassword');

            cy.get('input#email').type('costincometestuser@gmail');
            cy.get('input#password').type('password');
            cy.get('input#newPassword').type('password1');
            cy.get('form').submit();

            cy.wait('@changePassword');

            cy.get('input#email').should('have.class', 'erroredFormField');
            cy.get('span').contains('User with specified email not exist').should('be.visible');
        })

        it("Submit with equals passwords", () => {
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('password');
            cy.get('input#newPassword').type('password');
            cy.get('form').submit();
            cy.get('input#newPassword').should('have.class', 'erroredFormField');
            cy.get('span').contains('The new password can\'t match old');
        })
    
    })

    describe("Change password with valid data", () => {

        beforeEach(() => {
            cy.visit('/changepassword');
        })

        after(() => {
            cy.log('Change password to default')
            cy.visit('/changepassword');
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('passwordtest');
            cy.get('input#newPassword').type('password');
            cy.get('form').submit();
        })

        it("Submit with valid data", () => {
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('password');
            cy.get('input#newPassword').type('passwordtest');
            cy.get('form').submit();
            cy.location('pathname').should('eq', '/authorization').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.null;
            })
            cy.get('input#email').type('costincometestuser@gmail.com');
            cy.get('input#password').type('passwordtest');
            cy.get('form').submit();
            cy.location('pathname').should('eq', '/home').should(() => {
                expect(
                    sessionStorage.getItem('token')
                ).to.be.string;
            });
        })
    })
})