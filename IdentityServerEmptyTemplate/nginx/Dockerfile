FROM nginx

#EXPOSE 443

COPY ./localhost.crt /etc/nginx/
COPY ./localhost.key /etc/nginx/
COPY ./localhost.pem /etc/nginx/
COPY ./localhost-key.pem /etc/nginx/
COPY ./nginx.conf /etc/nginx/

CMD ["nginx", "-g", "daemon off;"]