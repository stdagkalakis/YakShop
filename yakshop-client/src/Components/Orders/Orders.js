import React, { useState, forwardRef, useEffect, useImperativeHandle } from 'react'
import { Card } from 'semantic-ui-react'
import Order from './Order';
import axios from 'axios'

const Orders = forwardRef((props, ref) => {
    const [orders, setOrders] = useState([])
    useEffect(() => {
        getOrders();
    }, [])

    useImperativeHandle(ref, () => ({
        alertOrders() {
            getOrders();
        }
    }));
    const getOrders = async () => {
        await axios
            .get("/yak-shop/order", {
                headers: {
                    crossdomain: true
                }
            })
            .then(response => {
                setOrders(response.data);
            })
            .catch(error => {
                console.log(error);
            });
    }

    return (
        <div>
            <Card fluid>
                <Card.Content >
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <h2>Orders</h2>
                    </div>
                </Card.Content>
                <Card.Content>
                    {orders && (<Card.Group itemsPerRow={4}>
                        {orders.map((o, i) => {
                            return <Order orderDetails={o} key={i} />
                        })}
                    </Card.Group >)
                    }
                </Card.Content>
            </Card>
        </div >
    )
})

export default Orders;