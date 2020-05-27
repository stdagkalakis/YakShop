import React, { useState } from 'react'
import { Form, Button, Segment, Message, Transition } from 'semantic-ui-react'
import axios from 'axios'

const AddOrder = ({ notifyOrders }) => {
    const [orderDetail, setOrderDetail] = useState({
        name: '',
        milk: '',
        skins: '',
        days: ''
    });

    const [hidden, setHidden] = useState(false);
    const [orderRes, setOrderRes] = useState({ header: '', type: '', content: '' });


    const onChange = (e) => {
        setOrderDetail({
            ...orderDetail,
            [e.target.name]: e.target.value
        });
    }

    const resetForm = () => {
        setOrderDetail({
            name: '',
            milk: '',
            skins: '',
            days: ''
        });
    }
    const onSubmit = e => {

        // prevent defalut not working.
        if ((orderDetail.milk === '' && orderDetail.skins === '') || orderDetail.name === '' || orderDetail.days === '') { resetForm(); return; }
        // Add yak to db.
        placeOrder();

    }

    const placeOrder = async () => {
        let data = {
            "customer": orderDetail.name,
            "order": {
                "milk": (orderDetail.milk === '') ? 0 : parseFloat(orderDetail.milk),
                "skins": (orderDetail.skins === '') ? 0 : parseInt(orderDetail.skins)
            }
        };

        await axios.post(`/yak-shop/order/${orderDetail.days}`, data, { headers: { "Content-Type": "application/json" } })
            .then(res => {
                var skins = (res.data.skins) ? `${res.data.skins} skins` : '';
                var milk = (res.data.milk) ? `${res.data.milk} liters of milk` : '';


                if (res.status === 206) {
                    setOrderRes({
                        header: 'Partially placed order!',
                        type: 'warning',
                        content: `Your order has been partially placed succefully! Placed Order: ${skins} ${milk}`
                    });
                } else if (res.status === 201) {

                    setOrderRes({
                        header: 'Placed order!',
                        type: 'success',
                        content: `Your full order has been placed succefully! Placed Order: ${skins} ${milk}`
                    })
                }

                setHidden(true);
                setTimeout(() => setHidden(false), 5000);
                notifyOrders();
                resetForm();
            }).catch(error => {

                if (error.response.status === 404) {
                    setOrderRes({
                        header: 'Order not placed!',
                        type: 'negative',
                        content: `We were unable to place your order, due to sortage of stock.`
                    });
                }
                setHidden(true);
                setTimeout(() => setHidden(false), 5000);
            });
    }
    return (
        <Segment.Group >
            <Segment textAlign='left'>
                <Form style={{ margin: '0.8rem' }} onSubmit={onSubmit} >
                    <Form.Group widths='4'>
                        <Form.Input fluid label='Cutomer name' name='name' type="text" value={orderDetail.name} placeholder='Customer name...' onChange={onChange} />
                        <Form.Input fluid label="Skins" name="skins" type="number" value={orderDetail.skins} min='0' step="1" onChange={onChange} />
                        <Form.Input fluid label="Milk" name="milk" type="number" value={orderDetail.milk} min='0' step="0.01" onChange={onChange} />
                        <Form.Input fluid label="Days" name="days" type="number" value={orderDetail.days} min='0' step="1" onChange={onChange} />
                    </Form.Group>

                    <Form.Field control={Button} type="submit" primary>Place order</Form.Field>

                </Form>
                <Transition visible={hidden} animation='fade down' duration={700}>
                    <Message
                        id='message'
                        className={orderRes.type}
                        header={orderRes.header}
                        content={orderRes.content}
                    />
                </Transition>
            </Segment>
        </Segment.Group>
    )
}

export default AddOrder;
