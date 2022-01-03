import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { Text, View } from 'react-native';

export default () => {
    return (<>
        <Text>Cześć Krzysio!</Text>
        <Text>It looks like it's working.</Text>
        <StatusBar style="auto" />
    </>);
}