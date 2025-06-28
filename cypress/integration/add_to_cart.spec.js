describe('Add to Cart Functionality', () => {
  beforeEach(() => {
    cy.visit('/');
  });

  it('should add a book to cart and show correct cart count', () => {
    // Search for "react" books
    cy.get('[data-testid="search-input"]').type('react');
    cy.get('[data-testid="search-button"]').click();

    // Wait for books to load
    cy.get('[data-testid="book-card"]').should('be.visible');

    // Click first "Add to Cart" button
    cy.get('[data-testid="add-to-cart-button"]').first().click();

    // Check if header shows cart count
    cy.get('[data-testid="cart-count"]').should('contain', '1');

    // Go to cart page
    cy.get('[data-testid="cart-link"]').click();

    // Verify cart page shows the book
    cy.get('[data-testid="cart-item"]').should('have.length', 1);
    cy.get('[data-testid="checkout-button"]').should('be.visible');
  });

  it('should be able to remove items from cart', () => {
    // Add a book to cart first
    cy.get('[data-testid="search-input"]').type('javascript');
    cy.get('[data-testid="search-button"]').click();
    cy.get('[data-testid="book-card"]').should('be.visible');
    cy.get('[data-testid="add-to-cart-button"]').first().click();

    // Go to cart
    cy.get('[data-testid="cart-link"]').click();

    // Remove the item
    cy.get('[data-testid="remove-button"]').click();

    // Verify cart is empty
    cy.get('[data-testid="empty-cart-message"]').should('be.visible');
  });
}); 