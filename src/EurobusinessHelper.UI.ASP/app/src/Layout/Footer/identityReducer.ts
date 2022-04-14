import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const identitySlice = createSlice({
    name: 'identity',
    initialState: {},
    reducers: {
        setIdentity: (state, action: PayloadAction<any>) => {
            state = action.payload;
        }
    }
});

export const { setIdentity } = identitySlice.actions;

export default identitySlice.reducer;