import React, { Fragment, useRef } from 'react'
import Navigation from '../Layout/Navigation';
import AddOrder from '../Orders/AddOrder';
import Orders from '../Orders/Orders';

const Customer = ({ props }) => {
    const orderRef = useRef();

    const notifyOrders = () => {
        if (orderRef !== null) orderRef.current.alertOrders();
    }
    return (
        <Fragment>
            <Navigation {...props} />
            <AddOrder notifyOrders={notifyOrders} />
            <Orders ref={orderRef} />
        </Fragment>
    )
}

export default Customer
