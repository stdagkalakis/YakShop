import React, { useState, useEffect, forwardRef, useImperativeHandle } from 'react'
import { Card, Input } from 'semantic-ui-react'
import axios from 'axios'
import Yak from './Yak'

const Herd = forwardRef((props, ref) => {

    const [herd, setHerd] = useState([]);
    const [days, setDays] = useState(0);

    useEffect(() => {
        getHerd(days);
        // eslint-disable-next-line
    }, [])

    useImperativeHandle(ref, () => ({

        alertHerd() {
            getHerd();
        }

    }));

    const onChange = e => {
        setDays(e.target.value);

    }
    const getHerd = async (days) => {
        const urlString = (days === 0 || days === undefined) ? `/yak-shop/herd` : `/yak-shop/herd/${days}`;
        await axios
            .get(urlString, {
                headers: {
                    crossdomain: true
                }
            })
            .then(response => {
                setHerd(response.data.herd);
            })
            .catch(error => {
                console.log(error);

            });

    };


    return (
        <div>
            <Card fluid>
                <Card.Content >
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <h2>Herd</h2>
                        <Input action={{ icon: 'play', onClick: (e, data) => { getHerd(days) } }} name="days" type="number" min='0' step="1" onChange={onChange} placeholder='Preview herd in days...' />
                    </div>
                </Card.Content>
                <Card.Content>
                    {(herd && <Card.Group itemsPerRow={4}>
                        {herd.map((y, i) => {
                            return <Yak key={i} yak={y} />
                        })}
                    </Card.Group >)
                    }
                </Card.Content>
            </Card>

        </div >
    )
})

export default Herd