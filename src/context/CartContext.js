import React, { createContext, useContext, useReducer, useEffect } from 'react';

const CartContext = createContext();

// Cart reducer actions
const CART_ACTIONS = {
  ADD_TO_CART: 'ADD_TO_CART',
  REMOVE_FROM_CART: 'REMOVE_FROM_CART',
  CLEAR_CART: 'CLEAR_CART'
};

// Cart reducer function
const cartReducer = (state, action) => {
  switch (action.type) {
    case CART_ACTIONS.ADD_TO_CART:
      const existingItem = state.cartItems.find(item => item.id === action.payload.id);
      
      if (existingItem) {
        // If item exists, increase quantity
        return {
          ...state,
          cartItems: state.cartItems.map(item =>
            item.id === action.payload.id
              ? { ...item, quantity: item.quantity + 1 }
              : item
          )
        };
      } else {
        // If item doesn't exist, add it with quantity 1
        return {
          ...state,
          cartItems: [...state.cartItems, { ...action.payload, quantity: 1 }]
        };
      }

    case CART_ACTIONS.REMOVE_FROM_CART:
      return {
        ...state,
        cartItems: state.cartItems.filter(item => item.id !== action.payload)
      };

    case CART_ACTIONS.CLEAR_CART:
      return {
        ...state,
        cartItems: []
      };

    default:
      return state;
  }
};

// Get initial state from localStorage
const getInitialState = () => {
  try {
    const savedCart = localStorage.getItem("myCart");
    if (savedCart) {
      const parsedCart = JSON.parse(savedCart);
      return {
        cartItems: Array.isArray(parsedCart) ? parsedCart : []
      };
    }
  } catch (error) {
    console.error("Error parsing cart from localStorage:", error);
  }
  return {
    cartItems: []
  };
};

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};

export const CartProvider = ({ children }) => {
  const [state, dispatch] = useReducer(cartReducer, getInitialState());

  // Sync cartItems to localStorage whenever it changes
  useEffect(() => {
    try {
      localStorage.setItem("myCart", JSON.stringify(state.cartItems));
    } catch (error) {
      console.error("Error saving cart to localStorage:", error);
    }
  }, [state.cartItems]);

  const addToCart = (book) => {
    dispatch({ type: CART_ACTIONS.ADD_TO_CART, payload: book });
  };

  const removeFromCart = (bookId) => {
    dispatch({ type: CART_ACTIONS.REMOVE_FROM_CART, payload: bookId });
  };

  const clearCart = () => {
    dispatch({ type: CART_ACTIONS.CLEAR_CART });
  };

  const value = {
    cartItems: state.cartItems,
    addToCart,
    removeFromCart,
    clearCart
  };

  return (
    <CartContext.Provider value={value}>
      {children}
    </CartContext.Provider>
  );
}; 