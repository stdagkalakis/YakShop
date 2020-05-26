import React, { Fragment } from 'react'
import { Card, Image, Segment } from 'semantic-ui-react'
import logo from '../../img/YikYak.png';
const Order = ({ orderDetails }) => {
    const { customer, order } = orderDetails;
    return (
        <Fragment>
            {   // Check if age is less than 10.
                (<Card color='orange' fluid>
                    <Card.Content textAlign='left' >
                        <Image
                            floated='right'
                            size='mini'
                            src={logo}
                        />
                        <Card.Header>{customer}</Card.Header>
                        <Card.Meta>Order details:</Card.Meta>
                    </Card.Content>
                    <Card.Description >
                        <Segment.Group >
                            {order.milk && <Segment textAlign='left'>Milk: <strong>{order.milk}</strong></Segment>}
                            {order.skins && <Segment textAlign='left'>Skins: <strong>{order.skins}</strong></Segment>}
                        </Segment.Group >

                    </Card.Description>
                </Card >)
            }
        </Fragment>
    )
}

export default Order
