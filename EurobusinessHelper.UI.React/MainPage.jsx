import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { Provider } from 'react-redux';
import { Router } from 'react-router';
import { createStore } from "redux";
import initialState from "./store/initialState";
import root from "./store/reducers/root";
import Page from "./Page";

export default () => {
  let store = createStore(root);
  return (
    <View style={styles.container}>
      <Provider store={store}>
        <Page />
      </Provider>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
