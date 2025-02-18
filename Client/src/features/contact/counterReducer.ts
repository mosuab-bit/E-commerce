import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// Define the state type
export type CounterState = {
    data: number;
};

// Initial state
const initialState: CounterState = {
    data: 42
};

// Create a slice with modern reducers
export const counterSlice = createSlice({
    name: 'counter',
    initialState,
    reducers: {
        increment: (state, action: PayloadAction<number>) => {
            state.data += action.payload;
        },
        decrement: (state, action: PayloadAction<number>) => {
            state.data -= action.payload;
        }
    }
});

// Export the modern actions
export const { increment, decrement } = counterSlice.actions;

// Export the modern reducer
export default counterSlice.reducer;
