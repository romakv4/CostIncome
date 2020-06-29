context("Auth redirections", () => {
    describe("Go to page with redirect", () => {
        
        beforeEach(() => {
            cy.window().then((win) => {
                win.sessionStorage.clear();
            });
            cy.reload();
            cy.signIn('costincometestuser@gmail.com', 'password');
        })

        it("Try to go to authorization page if token not expired", () => {
            cy.go('back');
            cy.location('pathname').should('eq', '/home');
            cy.get('button').contains('Log out').click();
        })

        it("Try to go to authorization page if token is manually deleted", () => {
            cy.clearSessionStorage();
            cy.go('back');
            cy.location('pathname').should('eq', '/authorization');
        })

        it("Reload home page if token is manually deleted", () => {
            cy.clearSessionStorage();
            cy.reload();
            cy.location('pathname').should('eq', '/authorization');
        })

        it("Try to go to registration if token not expired", () => {
            cy.visit('/registration');
            cy.location('pathname').should('eq', '/home');
        })

        it("Try to go to registration if token is manually deleted", () => {
            cy.clearSessionStorage();
            cy.visit('/registration');
            cy.location('pathname').should('eq', '/registration');
        })

        it("Try to go to main page if token is not expired", () => {
            cy.visit('/');
            cy.location('pathname').should('eq', '/home');
        })

        it("Try to go to main page if token is manually deleted", () => {
            cy.clearSessionStorage();
            cy.visit('/');
            cy.location('pathname').should('eq', '/');
        })
    })
})