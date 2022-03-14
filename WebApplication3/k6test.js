import http from 'k6/http';
import { sleep } from 'k6';
export const options = {
    vus: 50,
    duration: '30s',
};
export default function () {
    const responses = http.batch([
        ['GET', 'https://localhost:7236/hello',  null ],
        //['GET', 'https://localhost:7236/bye',  null ],
        //['GET', 'https://localhost:7236/test',  null ]
        ['GET', 'https://localhost:7236/guid?greeting=gg',  null ]

    ])
    //http.get('https://localhost:7236/hello');
    //http.get('https://localhost:7236/bye');
    //http.get('https://localhost:7236/test');
}
