FROM openjdk:8-alpine

COPY target/uberjar/customer-service.jar /customer-service/app.jar

EXPOSE 3000

CMD ["java", "-jar", "/customer-service/app.jar"]
