
import React, { useRef } from 'react'

import Herd from '../Yak/Herd';
import AddYak from '../Yak/AddYak';
import Navigation from '../Layout/Navigation';

const Shepherd = ({ props }) => {
    const herdRef = useRef();

    const notifyHerd = () => {
        if (herdRef !== null) herdRef.current.alertHerd();
    }

    return (
        <div >
            <Navigation {...props} />
            <AddYak notifyHerd={notifyHerd} />
            <Herd ref={herdRef} />
        </div >
    )
}

export default Shepherd;