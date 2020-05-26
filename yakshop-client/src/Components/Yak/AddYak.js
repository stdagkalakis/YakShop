import React, { useState } from 'react'
import { Form, Button, Segment, Message, Transition } from 'semantic-ui-react'
import axios from 'axios'

const AddYak = ({ notifyHerd }) => {
    const [yak, setYak] = useState({
        name: '',
        age: '',
        sex: ''
    });
    // select options
    const options = [
        { key: 0, text: 'Female', value: 0 },
        { key: 1, text: 'Male', value: 1 }

    ]

    const [hidden, setHidden] = useState(false);
    const [addRes, setOrderRes] = useState({ header: '', type: '', content: '' });
    const onChange = (e) => {
        setYak({
            ...yak,
            [e.target.name]: e.target.value
        });
    }

    const onChangeDrop = (e, data) => {
        setYak({
            ...yak,
            [data.name]: data.value
        })
    }

    const resetForm = () => {
        setYak({
            name: '',
            age: '',
            sex: ''
        });
    }

    const postYakForAddition = async () => {

        let data = {
            "Name": yak.name,
            "Age": parseFloat(yak.age),
            "Sex": yak.sex,
            "LastShavedAge": (yak.age >= 1) ? parseFloat(yak.age) : 1.0
        };
        await axios.post(`/yak-shop/herd/`, data, { headers: { "Content-Type": "application/json" } })
            .then(res => {
                // Set message to success and message
                setOrderRes({
                    header: 'Yak Added!',
                    type: 'success',
                    content: `Your herd has grown with the addition of ${data.Name}`
                });
                // invoke transition and dissapear after 6 sec
                setHidden(true);
                setTimeout(() => setHidden(false), 6000);
                resetForm();
                //Update herd component
                notifyHerd();
            }).catch(error => {
                // Set message to error and notify user.
                setOrderRes({
                    header: 'Yak addition failed!',
                    type: 'negative',
                    content: `We were unable to add ${data.Name} to your herd.`
                });
                setHidden(true);
                setTimeout(() => setHidden(false), 6000);
            });
    }

    const onSubmit = e => {
        // prevent defalut not working.
        if (yak.sex === '' || yak.age === '' || yak.name === '') { resetForm(); return; }

        // Add yak to db.
        postYakForAddition();
    }
    return (
        <Segment.Group >
            <Segment textAlign='left'>
                <Form style={{ margin: '0.8rem' }} onSubmit={onSubmit} >
                    <Form.Group widths='3'>
                        <Form.Input fluid label='Yak name' name='name' type="text" value={yak.name} placeholder='Yak name...' onChange={onChange} />
                        <Form.Select fluid label='Yak sex' name='sex' value={yak.sex} placeholder='Gender' options={options} onChange={onChangeDrop} />
                        <Form.Input fluid label="Age" name="age" type="number" value={yak.age} min='0' step="0.01" onChange={onChange} />
                    </Form.Group>
                    <Form.Field control={Button} type="submit" primary>Add to herd</Form.Field>
                </Form>
                <Transition visible={hidden} animation='fade down' duration={700}>
                    <Message
                        id='message'
                        className={addRes.type}
                        header={addRes.header}
                        content={addRes.content}
                    />
                </Transition>
            </Segment>
        </Segment.Group>
    )
}

export default AddYak;
